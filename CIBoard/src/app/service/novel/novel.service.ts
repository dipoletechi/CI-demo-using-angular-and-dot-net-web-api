import { Injectable } from '@angular/core';
import {ApiService} from '../Common/api.service';
import {UrlHelperService} from '../Common/urlhelper.service';
import {CreateNovelModel} from '../../models/novel/novel.model'

@Injectable({
  providedIn: 'root'
})
export class NovelService {

  constructor(private _api:ApiService, private CreateNovelModel : CreateNovelModel) { }


  createNovel(noveldetail)
  {
    return this._api.post(UrlHelperService.createnovel,noveldetail)
  }

  getavailablenovels()
  {
    return this._api.get(UrlHelperService.getavailablenovels)
  }

}
