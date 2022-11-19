using AutoMapper;
using Desafio_Backend.Domain.DTO.Livro;
using Desafio_Backend.Domain.DTO.User;
using Desafio_Backend.Domain.Identity;
using Desafio_Backend.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Desafio_Backend.Domain.Mapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Livro, LivroListarDto>()
                .ForMember(dest => dest.dataCadastro, opt => opt.MapFrom(src => src.dataCadastro.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero.nome))
                .ForMember(dest => dest.Editora, opt => opt.MapFrom(src => src.Editora.nome))
                .ForMember(dest => dest.Autores, opt => opt.MapFrom(src => src.Livro_Autores.Select(x => x.Autor.nome)));

            CreateMap<LivroAdicionarDto, Livro>()
                .AfterMap((src, dest) =>
                    {
                        dest.Livro_Autores = new List<Livro_Autor>();
                        foreach(int idAutor in src.idAutores)
                        {
                            Livro_Autor livroAutor = new Livro_Autor();
                            livroAutor.idAutor = idAutor;
                            dest.Livro_Autores.Add(livroAutor);
                        }
                    }
                );

            CreateMap<LivroEditarDto, Livro>()
                .ForMember(dest => dest.nome, opt => opt.Condition(src => src.nome != null))
                .ForMember(dest => dest.descricao, opt => opt.Condition(src => src.descricao != null))
                .ForMember(dest => dest.edicao, opt => opt.Condition(src => src.edicao != null))
                .ForMember(dest => dest.anoPublicacao, opt => opt.Condition(src => src.anoPublicacao != null))
                .ForMember(dest => dest.urlCapa, opt => opt.Condition(src => src.urlCapa != null))
                .ForMember(dest => dest.valor, opt => opt.Condition(src => src.valor != null))
                .ForMember(dest => dest.avaliacao, opt => opt.Condition(src => src.avaliacao != null))
                .ForMember(dest => dest.idGenero, opt => opt.Condition(src => src.idGenero != null))
                .ForMember(dest => dest.idEditora, opt => opt.Condition(src => src.idEditora != null));

            CreateMap<UserAdicionarDto, User>();

            CreateMap<UserLoginDto, User>();

        }
    }
}
