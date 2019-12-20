import { Injectable } from '@angular/core';
import {ApiService} from '../Common/api.service';
import {UrlHelperService} from '../Common/urlhelper.service';
import {CreateGenreModel} from '../../models/genre/genre.model'

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  constructor(private _api:ApiService, private CreateGenreModel : CreateGenreModel) { }


  createGenre(genredetail)
  {
    return this._api.post(UrlHelperService.creategenre,genredetail)
  }

  deletegenre(genredetail)
  {
    return this._api.post(UrlHelperService.deletegenre,genredetail)
  }

  getallgenres()
  {
    return this._api.get(UrlHelperService.getallgenres)
  }

  searchGenre(genredetail)
  {
    return this._api.post(UrlHelperService.getgenressearchedbyuser,genredetail)
  }

}
