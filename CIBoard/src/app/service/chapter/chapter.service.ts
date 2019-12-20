import { Injectable } from '@angular/core';
import {ApiService} from '../Common/api.service';
import {UrlHelperService} from '../Common/urlhelper.service';
import {CreateChapterModel} from '../../models/chapter/chapter.model'

@Injectable({
  providedIn: 'root'
})
export class ChapterService {

  constructor(private _api:ApiService, private CreateChapterModel : CreateChapterModel) { }


  createChapter(chapterdetail)
  {
    return this._api.post(UrlHelperService.createchapter,chapterdetail)
  }

  getallchapters()
  {
    return this._api.get(UrlHelperService.getallchapters)
  }

  publishchapter(chapterdetail)
  {
    return this._api.post(UrlHelperService.publishchapter,chapterdetail)
  }

}
