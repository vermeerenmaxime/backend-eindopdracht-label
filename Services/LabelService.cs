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
        Task<Artist> GetArtistByArtistName(string artistName);
        Task<List<Artist>> GetArtists();
        Task<Recordlabel> GetRecordlabelByName(string labelName);
        Task<Recordlabel> AddRecordlabel(Recordlabel recordlabel);
        Task<List<Recordlabel>> GetRecordlabels();
        Task<Artist> GetArtistByArtistId(Guid artistId);
        Task<List<Song>> GetSongsBySongName(string songName);
        Task<Song> GetSongBySongId(Guid songId);
        Task<List<Song>> GetSongs();
        Task<SongAddDTO> AddSong(SongAddDTO song);
        Task<List<Song>> GetSongsByRecordlabelName(string labelName);
        Task<Recordlabel> GetRecordlabelById(Guid recordLabelId);
    }

    public class LabelService : ILabelService
    {
        private IArtistRepository _artistRepository;
        private IRecordlabelRepository _recordlabelRepository;
        private ISongRepository _songRepository;
        private IMapper _mapper;

        public LabelService(IMapper mapper, IArtistRepository artistRepository, IRecordlabelRepository recordlabelRepository, ISongRepository songRepository)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
            _recordlabelRepository = recordlabelRepository;
            _songRepository = songRepository;

        }
        public async Task<List<Artist>> GetArtists()
        {
            return await _artistRepository.GetArtists();
        }
        public async Task<Artist> GetArtistByArtistName(string artistName)
        {
            return await _artistRepository.GetArtistByArtistName(artistName);
        }
        public async Task<Artist> GetArtistByArtistId(Guid artistId)
        {
            return await _artistRepository.GetArtistByArtistId(artistId);
        }
        public async Task<Artist> AddArtist(Artist artist)
        {
            // voor DTO
            // Artist newArtist = _mapper.Map<Artist>(artist);

            await _artistRepository.AddArtist(artist);
            return artist;
        }
        public async Task<List<Song>> GetSongs()
        {

            return await _songRepository.GetSongs();
        }
        public async Task<Song> GetSongBySongId(Guid songId)
        {

            return await _songRepository.GetSongBySongId(songId);
        }
        public async Task<List<Song>> GetSongsBySongName(string songName)
        {

            return await _songRepository.GetSongsBySongName(songName);
        }
        public async Task<SongAddDTO> AddSong(SongAddDTO song)
        {
            try
            {
                // voor DTO
                Song newSong = _mapper.Map<Song>(song);
                try
                {
                    newSong.SongId = Guid.NewGuid();
                    List<Artist> artists = new List<Artist>();
                    foreach (var artistId in song.ArtistIds)
                    {

                        // Artist artist = await GetArtistByArtistId(artistId);
                        // artist.ArtistId = Guid.NewGuid();
                        // artists.Add(artist);

                        await _songRepository.AddSongArtist(new SongArtist()
                        {
                            SongArtistId = Guid.NewGuid(),
                            SongId = newSong.SongId,
                            ArtistId = artistId,
                        });
                    }
                    try
                    {
                        Recordlabel recordlabel = await GetRecordlabelById(song.RecordLabelId);

                        newSong.LabelId = recordlabel.RecordLabelId;

                        await _songRepository.AddSong(newSong);
                        return song;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<List<Song>> GetSongsByRecordlabelName(string labelName)
        {
            Recordlabel recordlabel = await _recordlabelRepository.GetRecordlabelByName(labelName);

            List<Song> songsByRecordlabel = new List<Song>();

            foreach (var song in await _songRepository.GetSongs())
            {
                if (song.LabelId == recordlabel.RecordLabelId)
                {
                    songsByRecordlabel.Add(song);
                }
            }
            return songsByRecordlabel;
        }
        public async Task<List<Recordlabel>> GetRecordlabels()
        {

            return await _recordlabelRepository.GetRecordlabels();
        }

        public async Task<Recordlabel> GetRecordlabelByName(string labelName)
        {
            return await _recordlabelRepository.GetRecordlabelByName(labelName);
        }
        public async Task<Recordlabel> GetRecordlabelById(Guid recordLabelId)
        {
            return await _recordlabelRepository.GetRecordlabelById(recordLabelId);
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
