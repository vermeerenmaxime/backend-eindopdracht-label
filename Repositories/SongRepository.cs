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
            try
            {
                return await _context.Songs.ToListAsync();

            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Song> AddSong(Song song)
        {
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();
            return song;
        }
        public async Task<SongArtist> AddSongArtist(SongArtist songArtist)
        {
            await _context.SongArtists.AddAsync(songArtist);
            await _context.SaveChangesAsync();
            return songArtist;
        }
        public async Task<List<Song>> GetSongsBySongName(string songName)
        {
            try
            {
                List<Song> songsBySongName = new List<Song>();
                List<Song> songs = new List<Song>();
                songs = await _context.Songs.ToListAsync();

                foreach (var song in songs)
                {
                    songsBySongName.Add(await _context.Songs.Where(s => s.SongName == songName).SingleOrDefaultAsync());
                }
                return songsBySongName;
                // return await _context.Songs.ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Song> GetSongBySongId(Guid songId)
        {
            try
            {
                return await _context.Songs.Where(s => s.SongId == songId).SingleOrDefaultAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
    }
}
