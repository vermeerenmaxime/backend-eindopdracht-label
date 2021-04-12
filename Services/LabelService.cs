using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Label.API.Models;
using Label.API.DTO;
using Label.API.Repositories;

namespace Label.API.Services
{
    public interface ILabelService
    {

        Task<Artist> AddArtist(Artist artist);
        Task<Recordlabel> AddRecordlabel(Recordlabel recordlabel);
        Task<List<Artist>> GetArtists();
        Task<List<Recordlabel>> GetRecordlabels();
    }

    public class LabelService : ILabelService
    {
        private IArtistRepository _artistRepository;
        private IRecordlabelRepository _recordlabelRepository;
        private IMapper _mapper;

        public LabelService(IMapper mapper, IArtistRepository artistRepository, IRecordlabelRepository recordlabelRepository)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
            _recordlabelRepository = recordlabelRepository;

        }
        public async Task<List<Artist>> GetArtists()
        {
            return await _artistRepository.GetArtists();
        }
        public async Task<Artist> AddArtist(Artist artist)
        {
            // voor DTO
            // Artist newArtist = _mapper.Map<Artist>(artist);

            await _artistRepository.AddArtist(artist);
            return artist;
        }
        public async Task<List<Recordlabel>> GetRecordlabels()
        {

            return await _recordlabelRepository.GetRecordlabels();
        }
        public async Task<Recordlabel> AddRecordlabel(Recordlabel recordlabel)
        {
            // voor DTO
            // Artist newArtist = _mapper.Map<Artist>(artist);

            await _recordlabelRepository.AddRecordlabel(recordlabel);
            return recordlabel;
        }

    }
}
