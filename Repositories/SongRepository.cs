using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Label.API.DataContext;
using Label.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Label.API.Repositories
{
    public interface ISongRepository
    {
        Task<List<Song>> GetSongs();
        Task<List<Song>> GetSongsBySongName(string songName);
        Task<Song> AddSong(Song song);
        Task<Song> GetSongBySongId(Guid songId);
        Task<SongArtist> AddSongArtist(SongArtist songArtist);
        Task<List<SongArtist>> GetSongArtistsBySongId(Guid songId);
    }

    public class SongRepository : ISongRepository
    {
        private ILabelContext _context;

        public SongRepository(ILabelContext context)
        {
            _context = context;
        }

        public async Task<List<Song>> GetSongs()
        {

            List<Song> songs = await _context.Songs.ToListAsync();

            return songs;

        }

        public async Task<Song> AddSong(Song song)
        {
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();
            return song;
        }
        public async Task<List<SongArtist>> GetSongArtistsBySongId(Guid songId)
        {
            return await _context.SongArtists.Where(s => s.SongId == songId).ToListAsync();
        }
        public async Task<SongArtist> AddSongArtist(SongArtist songArtist)
        {
            await _context.SongArtists.AddAsync(songArtist);
            await _context.SaveChangesAsync();
            return songArtist;
        }   
        public async Task<List<Song>> GetSongsBySongName(string songName)
        {
            return await _context.Songs.Where(s => s.SongName == songName).ToListAsync();

        }
        public async Task<Song> GetSongBySongId(Guid songId)
        {
            try
            {
                return await _context.Songs.Where(a => a.SongId == songId).SingleOrDefaultAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
    }
}
