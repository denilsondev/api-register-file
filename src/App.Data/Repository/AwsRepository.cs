using Amazon.S3;
using Amazon.SQS;
using Amazon.SQS.Model;
using App.Data.Interfaces;
using App.Domain.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace App.Data
{
    public class AwsRepository : IAwsRepository
    {
        AmazonS3Client s3Client = new AmazonS3Client(Environment.GetEnvironmentVariable("AWS_ACCESS_KEY"), Environment.GetEnvironmentVariable("AWS_SECRET_KEY"), Amazon.RegionEndpoint.USEast1);
        AmazonSQSClient sqsClient = new AmazonSQSClient(Environment.GetEnvironmentVariable("AWS_ACCESS_KEY"), Environment.GetEnvironmentVariable("AWS_SECRET_KEY"), Amazon.RegionEndpoint.USEast1);
        string bucketName = Environment.GetEnvironmentVariable("AWS_S3_BUCKET_NAME");
        string queueUrl = Environment.GetEnvironmentVariable("AWS_SQS_URL");

        public async Task<string> SalvaArquivo(Arquivo arquivo)
        {
            var result = "";
            var arquivoByte = Convert.FromBase64String(arquivo.ArquivoUpload);
            try
            {
                var request = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = arquivo.ArquivoNome,
                    CannedACL = S3CannedACL.PublicRead
                };

                using (var ms = new MemoryStream(arquivoByte))
                {
                    request.InputStream = ms;
                    await s3Client.PutObjectAsync(request);
                };

                result = string.Format("http://{0}.s3.amazonaws.com/{1}", bucketName, arquivo.ArquivoNome);

                MensagemArquivo mensagem = new MensagemArquivo()
                {
                    IdArquivo = arquivo.IdArquivo,
                    NomeArquivo = arquivo.ArquivoNome,
                    UrlArquivo = result
                };

                EnviaMensagemAsync(mensagem);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Problema ao gravar arquivo - {ex.Message.ToString()}");
            }
        }


        public void EnviaMensagemAsync(MensagemArquivo mensagem)
        {
            try
            {
                string message = JsonConvert.SerializeObject(mensagem);
                var request = new SendMessageRequest(queueUrl, message);

                var result = sqsClient.SendMessageAsync(request);
            }
            catch (Exception ex)
            {
                throw new Exception($"Problema ao enviar mensagem - {ex.Message.ToString()}");
            }
        }
    }
}
