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

        Task<Artist> GetArtistByArtistName(string artistName);
        Task<List<Artist>> GetArtists();
        Task<Artist> AddArtist(Artist artist);
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
        Task<AlbumDTO> AddAlbum(AlbumDTO album);
        Task<List<Album>> GetAlbumsByArtistName(string artistName);
        Task<List<Album>> GetAlbumsByAlbumName(string albumName);
        Task<Artist> UpdateArtist(Artist artist);
        Task<SongUpdateDTO> UpdateSong(SongUpdateDTO song);
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
        #region Artist
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
            await _artistRepository.AddArtist(artist);
            return artist;
        }
        public async Task<Artist> UpdateArtist(Artist artist)
        {
            await _artistRepository.UpdateArtist(artist);
            return artist;
        }

        #endregion
        #region Songs
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
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
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
                newSong.SongId = Guid.NewGuid();

                foreach (var artistId in song.ArtistIds)
                {

                    Artist artist = await GetArtistByArtistId(artistId);

                    if (artist != null)
                    {
                        await _songRepository.AddSongArtist(new SongArtist()
                        {
                            SongArtistId = Guid.NewGuid(),
                            SongId = newSong.SongId,
                            ArtistId = artistId,
                        });
                    }
                }

                Recordlabel recordlabel = await GetRecordlabelById(song.RecordLabelId);
                if (recordlabel != null) newSong.LabelId = recordlabel.RecordLabelId;

                await _songRepository.AddSong(newSong);
                return song;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SongUpdateDTO> UpdateSong(SongUpdateDTO song)
        {
            try
            {
                Song newSong = _mapper.Map<Song>(song);
                await _songRepository.UpdateSong(newSong);
                return song;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Song>> GetSongsByRecordlabelName(string labelName)
        {
            try
            {
                Recordlabel recordlabel = await _recordlabelRepository.GetRecordlabelByName(labelName);
                if (recordlabel != null)
                {
                    List<Song> songsByRecordlabel = new List<Song>();
                    List<Song> songs = await GetSongs();
                    if (songs != null)
                    {
                        foreach (var song in songs)
                        {
                            if (song.LabelId == recordlabel.RecordLabelId)
                            {
                                songsByRecordlabel.Add(song);
                            }
                        }
                        return songsByRecordlabel;
                    }
                    else throw new ArgumentException("Database has no songs yet");
                }
                else throw new ArgumentException("Recordlabel doesn't exist");
                // return await _songRepository.GetSongsByRecordlabelName(labelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        #region Recordlabel
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
        #endregion
        #region Album
        public async Task<List<Album>> GetAlbums()
        {
            List<Album> albums = await _albumRepository.GetAlbums();
            foreach (var album in albums)
            {
                List<AlbumSong> albumSongs = await _albumRepository.GetAlbumSongsByAlbumId(album.AlbumId);
                if (albumSongs != null)
                {
                    foreach (var albumSong in albumSongs)
                    {
                        Song song = await GetSongBySongId(albumSong.SongId);
                        if (song != null)
                        {
                            album.Songs.Add(song);
                        }

                    }
                }

            }
            return albums;
        }
        public async Task<Album> GetAlbumByAlbumId(Guid albumId)
        {
            try
            {
                Album album = await _albumRepository.GetAlbumByAlbumId(albumId);
                List<AlbumSong> albumSongs = await _albumRepository.GetAlbumSongsByAlbumId(album.AlbumId);
                album.Songs = new List<Song>();
                foreach (var song in album.Songs)
                {
                    List<Song> songs = new List<Song>();
                    album.Songs = songs;
                }

                return album;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Album>> GetAlbumsByArtistName(string artistName)
        {
            try
            {
                Artist artist = await _artistRepository.GetArtistByArtistName(artistName);
                List<Album> albums = new List<Album>();
                if (artist != null)
                {
                    albums = await _albumRepository.GetAlbumByArtistId(artist.ArtistId);
                }

                return albums;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Album>> GetAlbumsByAlbumName(string albumName)
        {

            return await _albumRepository.GetAlbumsByAlbumName(albumName);

        }
        public async Task<AlbumDTO> AddAlbum(AlbumDTO album)
        {
            try
            {
                Album newAlbum = _mapper.Map<Album>(album);
                newAlbum.AlbumId = Guid.NewGuid();
                newAlbum.Songs = new List<Song>();
                newAlbum.Artist = new Artist();

                foreach (var songId in album.SongIds)
                {
                    Song song = await GetSongBySongId(songId);
                    if (song != null)
                    {
                        await _albumRepository.AddAlbumSong(new AlbumSong()
                        {
                            AlbumSongId = Guid.NewGuid(),
                            SongId = songId,
                            AlbumId = newAlbum.AlbumId,
                        });

                    }

                }

                Artist artist = await GetArtistByArtistId(album.ArtistId);
                if (artist != null)
                {
                    newAlbum.ArtistId = artist.ArtistId;
                }

                await _albumRepository.AddAlbum(newAlbum);
                return album;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
