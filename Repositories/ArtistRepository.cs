using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Label.API.DataContext;
using Label.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Label.API.Repositories
{
    public interface IArtistRepository
    {
        Task<Artist> AddArtist(Artist artist);
        Task<List<Artist>> GetArtists();
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
        public async Task<Artist> AddArtist(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
            return artist;

        }
    }
}
