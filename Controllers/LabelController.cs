using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Label.API.Models;
using Label.API.DTO;
using Label.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Label.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class LabelController : ControllerBase
    {

        private readonly ILabelService _labelService;
        private readonly ILogger<LabelController> _logger;
        // 
        public LabelController(ILogger<LabelController> logger, ILabelService labelService)
        {
            _logger = logger;
            _labelService = labelService;
        }

        [HttpGet]
        [Route("artists")]
        public async Task<ActionResult<List<Artist>>> GetArtists()
        {
            try
            {
                return new OkObjectResult(await _labelService.GetArtists());
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        [HttpGet]
        [Route("artist/name/{artistName}")]
        public async Task<ActionResult<Artist>> GetArtistByArtistName(string artistName)
        {
            try
            {
                return new OkObjectResult(await _labelService.GetArtistByArtistName(artistName));

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        [HttpGet]
        [Route("artist/id/{artistId}")]
        public async Task<ActionResult<Artist>> GetArtistByArtistId(Guid artistId)
        {
            try
            {
                return new OkObjectResult(await _labelService.GetArtistByArtistId(artistId));

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            };
        }


        [HttpPost]
        [Route("artist")]
        public async Task<ActionResult<Artist>> AddArtist(Artist artist)
        {
            try
            {
                return new OkObjectResult(await _labelService.AddArtist(artist));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
            // return new Artist();
        }
        [HttpDelete]
        [Route("artist")]
        public async Task<ActionResult<Artist>> DeleteArtist(Artist artist)
        {
            try
            {
                return new OkObjectResult(await _labelService.DeleteArtist(artist));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
            // return new Artist();
        }

        [HttpGet]
        [Route("songs")]
        public async Task<ActionResult<List<Song>>> GetSongs()
        {
            try
            {
                return new OkObjectResult(await _labelService.GetSongs());
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }
        // ! List omdat er soms meerdere songs zijn met dezelfde naam!
        [HttpGet]
        [Route("song/name/{songName}")]
        public async Task<ActionResult<List<Song>>> GetSongsBySongName(string songName)
        {
            try
            {
                return new OkObjectResult(await _labelService.GetSongsBySongName(songName));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }

        }
        [HttpGet]
        [Route("song/id/{songId}")]
        public async Task<ActionResult<Song>> GetSongBySongId(Guid songId)
        {
            try
            {
                return new OkObjectResult(await _labelService.GetSongBySongId(songId));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        [HttpGet]
        [Route("songs/label/{labelName}")]
        public async Task<ActionResult<List<Song>>> GetSongsByRecordlabelName(string labelName)
        {
            try
            {
                return new OkObjectResult(await _labelService.GetSongsByRecordlabelName(labelName));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        [Route("song")]
        public async Task<ActionResult<SongAddDTO>> AddSong(SongAddDTO song)
        {
            try
            {
                return new OkObjectResult(await _labelService.AddSong(song));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("album/id/{albumId}")]
        public ActionResult<Album> GetAlbumByAlbumId()
        {
            return new Album();
        }
        [HttpGet]
        [Route("albums")]
        public async Task<ActionResult<List<Album>>> GetAlbums()
        {
            try
            {
                return new OkObjectResult(await _labelService.GetAlbums());
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        [HttpGet]
        [Route("albums/{artistName}")]
        public ActionResult<List<Album>> GetAlbumsByArtist()
        {
            return new List<Album>();
        }
        // ! Albums van artiesten kunnen dezelfde naam hebben
        [HttpGet]
        [Route("album/name/{albumName}")]
        public ActionResult<List<Album>> GetAlbumByAlbumName()
        {
            return new List<Album>();
        }
        [HttpPost]
        [Route("album")]
        public async Task<ActionResult<AlbumDTO>> AddAlbum(AlbumDTO album)
        {
            try
            {
                return new OkObjectResult(await _labelService.AddAlbum(album));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // [HttpPost]
        // [Route("album")]
        // public async Task<ActionResult<AlbumAddSongDTO>> AddSongToAlbum(AlbumAddSongDTO album)
        // {
        //     try
        //     {
        //         return new OkObjectResult(await _labelService.AddSongToAlbum(album));
        //     }
        //     catch (Exception ex)
        //     {
        //         throw ex;
        //     }
        // }
        [HttpGet]
        [Route("recordlabels")]
        public async Task<ActionResult<List<Recordlabel>>> GetRecordlabels()
        {
            try
            {
                return new OkObjectResult(await _labelService.GetRecordlabels());

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        [HttpGet]
        [Route("recordlabel/name/{labelName}")]
        public async Task<ActionResult<Recordlabel>> GetRecordlabelByLabelName(string labelName)
        {
            try
            {
                return new OkObjectResult(await _labelService.GetRecordlabelByName(labelName));

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        [HttpGet]
        [Route("recordlabel/id/{recordlabelId}")]
        public async Task<ActionResult<Recordlabel>> GetRecordlabelById(Guid recordlabelId)
        {
            try
            {
                return new OkObjectResult(await _labelService.GetRecordlabelById(recordlabelId));

            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        [HttpPost]
        [Route("recordlabel")]
        public async Task<ActionResult<Recordlabel>> AddLabel(Recordlabel recordlabel)
        {
            try
            {
                return new OkObjectResult(await _labelService.AddRecordlabel(recordlabel));
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

    }
}
