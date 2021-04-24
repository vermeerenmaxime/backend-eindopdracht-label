using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Label.API.DataContext;
using Label.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Label.API.Repositories
{
    public interface IArtistRepository
    {

        Task<Artist> AddArtist(Artist artist);
        Task<Artist> GetArtistByArtistName(string artistName);
        Task<List<Artist>> GetArtists();
        Task<Artist> GetArtistByArtistId(Guid artistId);
    }

    public class ArtistRepository : IArtistRepository
    {
        private ILabelContext _context;

        public ArtistRepository(ILabelContext context)
        {
            _context = context;
        }
        public async Task<List<Artist>> GetArtists()
        {
            try
            {
                return await _context.Artists.ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Artist> GetArtistByArtistName(string artistName)
        {
            try
            {
                return await _context.Artists.Where(a => a.ArtistName == artistName).SingleOrDefaultAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Artist> GetArtistByArtistId(Guid artistId)
        {
            try
            {
                return await _context.Artists.Where(a => a.ArtistId == artistId).SingleOrDefaultAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Artist> AddArtist(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
            return artist;

        }
    }
}
