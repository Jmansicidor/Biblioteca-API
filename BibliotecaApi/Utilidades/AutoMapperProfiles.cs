//using AutoMapper;
//using BibliotecaApi.DTOs;
//using BibliotecaApi.Entitys;

//namespace BibliotecaApi.Utilidades
//{
//	public class AutoMapperProfiles: Profile
//	{
//		public AutoMapperProfiles()
//		{
//			CreateMap<Autor, AutorDTO>()
//				.ForMember(dto => dto.NombreCompleto, config => config.MapFrom(autor => $"{autor.Name} {autor.SurName}"));
//		}
//	}
//}
