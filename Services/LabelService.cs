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
        Task<Artist> DeleteArtist(Artist artist);
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
        Task<AlbumAddSongDTO> AddSongToAlbum(AlbumAddSongDTO album);
        Task<List<Album>> GetAlbumsByArtistName(string artistName);
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
        public async Task<Artist> DeleteArtist(Artist artist)
        {

            await _artistRepository.DeleteArtist(artist);
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
                    else
                    {
                        // Artiest bestaat niet, maak eerst een artiest aan
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
        // public async Task<List<Album>> GetAlbums()
        // {
        //     List<Album> albums = await _albumRepository.GetAlbums();

        //     foreach (var album in albums)
        //     {
        //         Artist artist = await _artistRepository.GetArtistByArtistId(album.ArtistId);

        //         if (artist != null)
        //         {
        //             album.Artist = artist;
        //             List<Song> songs = new List<Song>();
        //             List<AlbumSong> albumSongs = await _albumRepository.GetAlbumSongsByAlbumId(album.AlbumId);
        //             if (albumSongs != null)
        //             {
        //                 foreach (var albumSong in albumSongs)
        //                 {
        //                     Song song = await GetSongBySongId(albumSong.SongId);
        //                     if (song != null) { songs.Add(song); }
        //                 }
        //                 album.Songs = songs;
        //             }
        //             else
        //             {
        //                 // Error test
        //                 // throw IActionResult<List<Song>>("Album bevat geen songs");
        //             }
        //         }
        //         else
        //         {
        //             // Error test
        //             // throw IActionResult<List<Song>>("Artist is niet gevonden");
        //         }

        //     }

        //     return albums;
        // }

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
        public async Task<List<Album>> GetAlbumsByArtistName(string artistName)
        {
            Artist artist = await _artistRepository.GetArtistByArtistName(artistName);
            List<Album> albums = new List<Album>();
            if (artist != null)
            {
                albums = await _albumRepository.GetAlbumByArtistId(artist.ArtistId);
            }

            return albums;
        }
        public async Task<AlbumDTO> AddAlbum(AlbumDTO album)
        {
            Album newAlbum = _mapper.Map<Album>(album);
            newAlbum.AlbumId = Guid.NewGuid();
            // newAlbum.Songs = new List<Song>();
            // newAlbum.Artist = new Artist();

            // foreach (var songId in album.SongIds)
            // {
            //     Song song = await GetSongBySongId(songId);
            //     if (song != null)
            //     {
            //         await _albumRepository.AddAlbumSong(new AlbumSong()
            //         {
            //             AlbumSongId = Guid.NewGuid(),
            //             SongId = songId,
            //             AlbumId = newAlbum.AlbumId,
            //         });

            //     }

            // }

            // Artist artist = await GetArtistByArtistId(album.ArtistId);
            // if (artist != null)
            // {
            //     newAlbum.ArtistId = artist.ArtistId;
            // }

            await _albumRepository.AddAlbum(newAlbum);
            return album;
        }

        // UNFINISHED UNTESTED
        public async Task<AlbumAddSongDTO> AddSongToAlbum(AlbumAddSongDTO album)
        {
            Album newAlbum = await _albumRepository.GetAlbumByAlbumId(album.AlbumId);
            Song newSong = await _songRepository.GetSongBySongId(album.SongId);
            if (newAlbum != null && newSong != null)
            {
                await _albumRepository.AddAlbumSong(new AlbumSong()
                {
                    AlbumSongId = Guid.NewGuid(),
                    SongId = album.SongId,
                    AlbumId = album.AlbumId,
                });
            }

            return album;
        }

    }
}
