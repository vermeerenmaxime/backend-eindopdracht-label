using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Label.API.DataContext;
using Label.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Label.API.Repositories
{
    public interface IRecordlabelRepository
    {
        Task<Recordlabel> AddRecordlabel(Recordlabel recordlabel);
        Task<List<Recordlabel>> GetRecordlabels();
    }

    public class RecordlabelRepository : IRecordlabelRepository
    {
        private ILabelContext _context;

        public RecordlabelRepository(ILabelContext context)
        {
            _context = context;
        }
        public async Task<List<Recordlabel>> GetRecordlabels()
        {
            try
            {
                return await _context.Recordlabels.ToListAsync();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
        public async Task<Recordlabel> AddRecordlabel(Recordlabel recordlabel)
        {
            await _context.Recordlabels.AddAsync(recordlabel);
            await _context.SaveChangesAsync();
            return recordlabel;
        }
    }
}
