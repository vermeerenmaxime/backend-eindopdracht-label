using System;
using AutoMapper;
using Label.API.Models;

namespace Label.API.DTO
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {

            CreateMap<AlbumDTO, Album>();
            CreateMap<Artist, ArtistDTO>();
            CreateMap<Recordlabel, RecordlabelDTO>();
            // CreateMap<Song, SongAddDTO>();
            CreateMap<SongAddDTO, Song>();

        }
    }
}
