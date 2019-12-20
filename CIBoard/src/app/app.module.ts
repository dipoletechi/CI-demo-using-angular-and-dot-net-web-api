import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { routingComponents } from './app-routing.module';
import {AuthGuard} from './guard/auth.guard';
import { HttpClientModule} from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { LoginModel } from './../app/models/login/login.model';
import { CreateNovelModel } from './../app/models/novel/novel.model';
import { CreateGenreModel, SearchGenreModel, DeleteeGenreModel } from './../app/models/genre/genre.model';
import { CreateChapterModel, PublishChapterModel } from './../app/models/chapter/chapter.model';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
     AppComponent,
    
      
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    routingComponents
  ],
  providers: [LoginModel, AuthGuard, CreateNovelModel, CreateGenreModel, CreateChapterModel, PublishChapterModel, SearchGenreModel, DeleteeGenreModel],
  bootstrap: [AppComponent]
})
export class AppModule {}
