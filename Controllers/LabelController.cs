using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Label.API.Models;
using Label.API.DTO;
using Label.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Label.API.Controllers
{
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
                // List<Artist> artists = new List<Artist>();
                // artists.Add(
                //     new Artist()
                //     {
                //         ArtistId = Guid.NewGuid(),
                //         ArtistName = "Eloy Hoose",
                //         Email = "Eloyhoose@gmail.com"
                //     }
                // );
                // artists.Add(
                //     new Artist()
                //     {
                //         ArtistId = Guid.NewGuid(),
                //         ArtistName = "Aten",
                //         Email = "Aten@gmail.com"
                //     }
                // );
                // return artists;
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
        [HttpGet]
        [Route("artist/{artistName}")]
        public ActionResult<Artist> GetArtistByArtistName()
        {
            return new Artist();
        }
        [HttpGet]
        [Route("artist/{artistId}")]
        public ActionResult<Artist> GetArtistByArtistId()
        {
            return new Artist();
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

        // ! List omdat er soms meerdere songs zijn met dezelfde naam!
        [HttpGet]
        [Route("song/{songName}")]
        public ActionResult<List<Song>> GetSongBySongName()
        {
            return new List<Song>();

        }
        [HttpGet]
        [Route("song/{songId}")]
        public ActionResult<Song> GetSongBySongId()
        {
            return new Song();
        }
        [HttpGet]
        [Route("songs/{recordlabelName}")]
        public ActionResult<List<Song>> GetSongsByRecordlabelName()
        {
            return new List<Song>();

        }
        [HttpPost]
        [Route("song")]
        public ActionResult<Song> AddSong()
        {
            return new Song();
        }
        [HttpGet]
        [Route("album/{albumId}")]
        public ActionResult<Album> GetAlbumByAlbumId()
        {
            return new Album();
        }
        [HttpGet]
        [Route("albums")]
        public ActionResult<List<Album>> GetAlbums()
        {
            return new List<Album>();
        }
        [HttpGet]
        [Route("albums/{artistName}")]
        public ActionResult<List<Album>> GetAlbumsByArtist()
        {
            return new List<Album>();
        }
        // ! Albums van artiesten kunnen dezelfde naam hebben
        [HttpGet]
        [Route("album/{albumName}")]
        public ActionResult<List<Album>> GetAlbumByAlbumName()
        {
            return new List<Album>();
        }
        [HttpPost]
        [Route("album")]
        public ActionResult<Album> AddAlbum()
        {
            return new Album();
        }
        [HttpGet]
        [Route("recordlabels")]
        public async Task<ActionResult<List<Recordlabel>>> GetRecordlabelsAsync()
        {
            // return new List<Recordlabel>();
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
        [Route("recordabel/{labelName}")]
        public ActionResult<Recordlabel> GetRecordlabelByLabelName()
        {
            return new Recordlabel();
        }
        [HttpPost]
        [Route("recordlabel")]
        public async Task<ActionResult<Recordlabel>> AddLabelAsync(Recordlabel recordlabel)
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
