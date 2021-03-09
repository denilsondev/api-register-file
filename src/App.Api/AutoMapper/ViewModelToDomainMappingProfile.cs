using App.Domain.Models;
using App.Domain.ViewModels;
using AutoMapper;

namespace App.Api.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CadastroViewModel, Arquivo>()
                .ConstructUsing(Arquivo => new Arquivo
                {
                    ArquivoNome = Arquivo.ArquivoNome,
                    ArquivoUpload = Arquivo.ArquivoUpload

                });
        }
    }
}