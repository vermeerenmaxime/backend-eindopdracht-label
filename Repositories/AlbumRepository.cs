using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Label.API.DataContext;
using Label.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Label.API.Repositories
{
    public interface IAlbumRepository
    {
        Task<List<Album>> GetAlbums();
    }

    public class AlbumRepository : IAlbumRepository
    {
        private ILabelContext _context;

        public AlbumRepository(ILabelContext context)
        {
            _context = context;
        }
        public async Task<List<Album>> GetAlbums()
        {
            try
            {
                return await _context.Albums.ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
    }
}
