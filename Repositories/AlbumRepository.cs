using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Label.API.DataContext;
using Label.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Label.API.Repositories
{
    public interface IAlbumRepository
    {
        Task<List<Album>> GetAlbums();
        Task<Album> GetAlbumByAlbumId(Guid albumId);
        Task<Album> AddAlbum(Album album);
        Task<List<AlbumSong>> GetAlbumSongsByAlbumId(Guid albumId);
        Task<AlbumSong> AddAlbumSong(AlbumSong albumSong);
        Task<List<Album>> GetAlbumByArtistId(Guid artistId);
        Task<List<Album>> GetAlbumsByAlbumName(string albumName);
    }

    public class AlbumRepository : IAlbumRepository
    {
        private ILabelContext _context;

        public AlbumRepository(ILabelContext context)
        {
            _context = context;
        }
        public async Task<Album> AddAlbum(Album album)
        {
            await _context.Albums.AddAsync(album);
            await _context.SaveChangesAsync();
            return album;
        }
        public async Task<AlbumSong> AddAlbumSong(AlbumSong albumSong)
        {
            await _context.AlbumSongs.AddAsync(albumSong);
            await _context.SaveChangesAsync();
            return albumSong;
        }
        public async Task<List<Album>> GetAlbums()
        {
            try
            {
                return await _context.Albums.Include(a => a.Artist).Include(a => a.Songs).ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Album> GetAlbumByAlbumId(Guid albumId)
        {
            try
            {
                return await _context.Albums.Where(a => a.AlbumId == albumId).SingleOrDefaultAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Album>> GetAlbumsByAlbumName(string albumName)
        {
            try
            {
                return await _context.Albums.Where(a => a.AlbumName == albumName).ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Album>> GetAlbumByArtistId(Guid artistId)
        {
            try
            {
                return await _context.Albums.Where(a => a.ArtistId == artistId).ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<AlbumSong>> GetAlbumSongsByAlbumId(Guid albumId)
        {
            try
            {
                return await _context.AlbumSongs.Where(a => a.AlbumId == albumId).ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
