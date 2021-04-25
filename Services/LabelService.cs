using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Label.API.Models;
using Label.API.DTO;
using Label.API.Repositories;
using Newtonsoft.Json;

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
        Task<List<Album>> GetAlbums();
    }

    public class LabelService : ILabelService
    {
        private IArtistRepository _artistRepository;
        private IRecordlabelRepository _recordlabelRepository;
        private ISongRepository _songRepository;

        private IAlbumRepository _albumRepository;
        private IMapper _mapper;

        public LabelService(IMapper mapper, IArtistRepository artistRepository, IRecordlabelRepository recordlabelRepository, ISongRepository songRepository, IAlbumRepository albumRepository)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
            _recordlabelRepository = recordlabelRepository;
            _songRepository = songRepository;
            _albumRepository = albumRepository;

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
            try
            {
                List<Song> songs = await _songRepository.GetSongs();
                foreach (var song in songs)
                {
                    List<SongArtist> songArtists = await _songRepository.GetSongArtistsBySongId(song.SongId);
                    song.Artists = new List<Artist>();
                    foreach (var songArtist in songArtists)
                    {
                        if (songArtist != null)
                        {
                            Artist artist = await _artistRepository.GetArtistByArtistId(songArtist.ArtistId);

                            if (artist != null) song.Artists.Add(artist);
                        }
                    }
                    song.Recordlabel = await _recordlabelRepository.GetRecordlabelById(song.LabelId);
                }
                return songs;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<Song> GetSongBySongId(Guid songId)
        {

            Song song = await _songRepository.GetSongBySongId(songId);
            List<SongArtist> songArtists = await _songRepository.GetSongArtistsBySongId(song.SongId);
            song.Artists = new List<Artist>();
            foreach (var songArtist in songArtists)
            {
                if (songArtist != null)
                {
                    Artist artist = await _artistRepository.GetArtistByArtistId(songArtist.ArtistId);

                    if (artist != null) song.Artists.Add(artist);
                }
            }
            song.Recordlabel = await _recordlabelRepository.GetRecordlabelById(song.LabelId);
            return song;
        }
        public async Task<List<Song>> GetSongsBySongName(string songName)
        {

            try
            {
                List<Song> songs = await _songRepository.GetSongsBySongName(songName);
                foreach (var song in songs)
                {
                    List<SongArtist> songArtists = await _songRepository.GetSongArtistsBySongId(song.SongId);
                    song.Artists = new List<Artist>();
                    foreach (var songArtist in songArtists)
                    {
                        if (songArtist != null)
                        {
                            Artist artist = await _artistRepository.GetArtistByArtistId(songArtist.ArtistId);

                            if (artist != null) song.Artists.Add(artist);
                        }
                    }
                    song.Recordlabel = await _recordlabelRepository.GetRecordlabelById(song.LabelId);
                }
                return songs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

                        Artist artist = await GetArtistByArtistId(artistId);
                        // artist.ArtistId = Guid.NewGuid();
                        // artists.Add(artist);

                        if (artist != null)
                        {
                            await _songRepository.AddSongArtist(new SongArtist()
                            {
                                SongArtistId = Guid.NewGuid(),
                                SongId = newSong.SongId,
                                ArtistId = artistId,
                            });
                        }
                        else
                        {
                            // Artiest bestaat niet, maak eerst een artiest aan
                        }

                    }
                    try
                    {
                        Recordlabel recordlabel = await GetRecordlabelById(song.RecordLabelId);
                        if (recordlabel != null) newSong.LabelId = recordlabel.RecordLabelId;

                        await _songRepository.AddSong(newSong);
                        return song;
                    }
                    catch (Exception ex)
                    {
                        // ex.Message = "JsonConvert.SerializeObject("error")";
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

            foreach (var song in await GetSongs())
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

            await _recordlabelRepository.AddRecordlabel(recordlabel);
            return recordlabel;
        }
        public async Task<List<Album>> GetAlbums()
        {
            return await _albumRepository.GetAlbums();
        }

    }
}
