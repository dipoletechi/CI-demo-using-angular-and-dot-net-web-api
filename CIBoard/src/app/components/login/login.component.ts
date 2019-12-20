import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginModel } from './../../models/login/login.model';
import { LoginService } from './../../service/login/login.service';
import { FormsModule } from '@angular/forms';
import { LocalstorageService } from './../../service/Common/localstorage.service';
import { commandservice } from 'src/app/service/Common/command.service';
import { CreateNovelModel } from './../../models/novel/novel.model';
import { NovelService } from './../../service/novel/novel.service';
import { CreateGenreModel, DeleteeGenreModel } from './../../models/genre/genre.model';
import { SearchGenreModel } from './../../models/genre/genre.model';
import { GenreService } from './../../service/genre/genre.service';
import { CreateChapterModel, PublishChapterModel } from './../../models/chapter/chapter.model';
import { ChapterService } from './../../service/chapter/chapter.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginDetail: any;  
  loginResponse: any;   
  publishChapterResponse;
  command:string;
  isLoading:boolean;
  currentRunningCommand:string;
  messageList:Array<string>;
  commandList:Array<string>;
  prepandText:string=" > ";
  novelModel:CreateNovelModel;
  createGenreModel: CreateGenreModel;
  searchGenreModel: SearchGenreModel;
  deleteGenreModel: DeleteeGenreModel;
  chapterModel: CreateChapterModel;
  publishChapterModel: PublishChapterModel;
  constructor(private localstorage: LocalstorageService,
        private _loginService: LoginService,
        private _novelService: NovelService,
        private _genreService: GenreService,
        private _chapterService: ChapterService) { 
        this.messageList=new Array<string>();
        this.commandList=new Array<string>();
        this.novelModel= new CreateNovelModel();
        this.createGenreModel = new CreateGenreModel();
        this.searchGenreModel = new SearchGenreModel();
        this.deleteGenreModel = new DeleteeGenreModel();
        this.chapterModel = new CreateChapterModel();
        this.publishChapterModel = new PublishChapterModel();
  }

  ngOnInit() {
  }

  sendCommand(){   
    if(this.command.toLowerCase()=="clear"){
      this.messageList=new Array<string>();      
    }

    if(this.command!="" && this.command!="clear"){
      this.messageList.push(this.prepandText+this.command);
      this.commandList.push(this.command);
    }  

    //login command
    if(this.command.indexOf(commandservice.login)>-1){     
      if( this.localstorage.getToken()){
        this.messageList.push(this.prepandText+"Already logged in");       
      }else{
        var splittedCommand=this.command.split(" ");
        if(splittedCommand.length==3){
            var username=this.command.split(" ")[1];
            var password=this.command.split(" ")[2];
            this.login(username,password);
        }else{
            this.messageList.push(this.prepandText+"Invalid command");
        }
      }                
    }
    //logout command
    else if(this.command.indexOf(commandservice.logout)>-1){
      this.localstorage.logout();
      if(this.localstorage.getToken()){
        this.messageList.push(this.prepandText+"Logged out successfully");       
      }
    }
    //create Novel
    if(this.command.indexOf(commandservice.createnovel)>-1){     
      if(this.localstorage.getToken()){
            var commanddata=this.command.split("$");
            if(commanddata.length==2){
                this.novelModel.Name=commanddata[1];
                this.createNovel(this.novelModel);
            }else{
              this.messageList.push(this.prepandText+"Invalid command");
            }
      }else{
        this.messageList.push(this.prepandText+"you are not authorized!");
      }
    }
    //create Genre
    if(this.command.indexOf(commandservice.creategenre)>-1){     
      if(this.localstorage.getToken()){
            var commanddataforgenre=this.command.split("$");
            if(commanddataforgenre.length==3){
              this.createGenreModel.NovelId = Number(commanddataforgenre[1]);
              this.createGenreModel.Title= commanddataforgenre[2];
              this.createGenre(this.createGenreModel);
            }else{
              this.messageList.push(this.prepandText+"Invalid command");
            }
      }else{
        this.messageList.push(this.prepandText+"you are not authorized!");
      }
    }
    // create Chapter
    if(this.command.indexOf(commandservice.createchapter)>-1){     
      if(this.localstorage.getToken()){
            var commanddataforchapter=this.command.split("$");
            if(commanddataforchapter.length==4){
              this.chapterModel.Novelid = Number(commanddataforchapter[1]);
              this.chapterModel.Name = commanddataforchapter[2];
              this.chapterModel.Content = commanddataforchapter[3];
              this.createChapter(this.chapterModel);
            }else{
              this.messageList.push(this.prepandText+"Invalid command");
            }
      }else{
        this.messageList.push(this.prepandText+"you are not authorized!");
      }
    }
    //get all novels
    if(this.command.indexOf(commandservice.getallnovels)>-1){
      var commanddataforgetallnovels=this.command.split("$");
      if(commanddataforgetallnovels.length==1){
        this.getavailablenovels();
      }else{
        this.messageList.push(this.prepandText+"Invalid command");
      }
    }
    //get all genres
    if(this.command.indexOf(commandservice.getallgenres)>-1){
      var commanddataforallgenres=this.command.split("$");
      if(commanddataforallgenres.length==1){
        this.getallgenres();
      }else{
        this.messageList.push(this.prepandText+"Invalid command");
      }
    }
    //get all chapters
    if(this.command.indexOf(commandservice.getallchapters)>-1){
      var commanddataforallchapter=this.command.split("$");
      if(commanddataforallchapter.length==1){
        this.getallchapters();
      }else{
        this.messageList.push(this.prepandText+"Invalid command");
      }
    }
    //search genre keyword in novels
    if(this.command.indexOf(commandservice.searchgenre)>-1){
      var commanddataforsearchgenreinnovels=this.command.split("$");
      if(commanddataforsearchgenreinnovels.length==2){
        this.searchGenreModel.Title= commanddataforsearchgenreinnovels[1];
        this.searchGenre(this.searchGenreModel);
      }else{
        this.messageList.push(this.prepandText+"Invalid command");
      }
    }
    //delete Genre
    if(this.command.indexOf(commandservice.deletegenre)>-1){     
      if(this.localstorage.getToken()){
        var commanddatafordeletegenre=this.command.split("$");
        if(commanddatafordeletegenre.length==3){
          this.deleteGenreModel.NovelId = Number(commanddatafordeletegenre[1]);
          this.deleteGenreModel.GenreTitle = commanddatafordeletegenre[2];
          this.deletegenre(this.deleteGenreModel);
        }else{
          this.messageList.push(this.prepandText+"Invalid command");
        }
      }else{
        this.messageList.push(this.prepandText+"you are not authorized!");
      }
    }
    //change publish status
    if(this.command.indexOf(commandservice.publishchapter)>-1){     
      if(this.localstorage.getToken()){
        var commanddataforpublishchapter=this.command.split("$");
        if(commanddataforpublishchapter.length==2){
          this.publishChapterModel.Id = Number(commanddataforpublishchapter[1]);
          this.publishchapter(this.publishChapterModel);
        }else{
          this.messageList.push(this.prepandText+"Invalid command");
        }
      }else{
        this.messageList.push(this.prepandText+"you are not authorized!");
      }
    }

    this.command="";
  }

  //login api
  login(username, password) {
    this.isLoading=true;
    this._loginService.login({username:username,password:password}).subscribe(res => {
      this.loginResponse = JSON.parse(res._body);         
      if (this.loginResponse.isSuccess) {            
        this.localstorage.setToken(this.loginResponse.responseData.token);                           
      }  
      this.messageList.push(this.prepandText+this.loginResponse.responseMessage); 
      this.isLoading=false;                   
    });
  }
  //createNovel api
  createNovel(novelName) {
    this.isLoading=true;
    this._novelService.createNovel(novelName).subscribe(res => {
      var novelCreateResponse = JSON.parse(res._body);          
      this.messageList.push(this.prepandText+novelCreateResponse.responseMessage); 
      this.isLoading=false;                   
    });
  }
  //createGenre api
  createGenre(genreModel) {
    this.isLoading=true;
    this._genreService.createGenre(genreModel).subscribe(res => {
      var genreCreateResponse = JSON.parse(res._body);
      this.messageList.push(this.prepandText+genreCreateResponse.responseMessage); 
      this.isLoading=false;                   
    });
  }
  //createChapter api
  createChapter(chapterModel) {
    this.isLoading=true;
    this._chapterService.createChapter(chapterModel).subscribe(res => {
      var genreCreateResponse = JSON.parse(res._body);
      this.messageList.push(this.prepandText+genreCreateResponse.responseMessage); 
      this.isLoading=false;                   
    });
  }
  //getallavailableNovels api
  getavailablenovels() {
    this.isLoading=true;
    this._novelService.getavailablenovels().subscribe(res => {
      var getAllNovelsResponse = JSON.parse(res._body);
      for(var i=0; i<getAllNovelsResponse.responseData.length; i++){
        this.messageList.push(getAllNovelsResponse.responseData[i].id +"  "+ getAllNovelsResponse.responseData[i].name);
      }
      this.isLoading=false;                  
    });
  }
  //getallavailableGenres api
  getallgenres() {
    this.isLoading=true;
    this._genreService.getallgenres().subscribe(res => {
      var getAllGenresResponse = JSON.parse(res._body);
      for(var i=0; i<getAllGenresResponse.responseData.length; i++){
        this.messageList.push(getAllGenresResponse.responseData[i].id +"  "+ getAllGenresResponse.responseData[i].title);
      }
      this.isLoading=false;                   
    });
  }
  //getallavailableChapters api
  getallchapters() {
    this.isLoading=true;
    this._chapterService.getallchapters().subscribe(res => {
      var getAllChapterResponse = JSON.parse(res._body);
      for(var i=0; i<getAllChapterResponse.responseData.length; i++){
        this.messageList.push(getAllChapterResponse.responseData[i].id +"  "+ getAllChapterResponse.responseData[i].name);
      }
      this.isLoading=false;                   
    });
  }
  //searchGenre that belongs to novels
  searchGenre(genresearchmodel) {
    this.isLoading=true;
    this._genreService.searchGenre(genresearchmodel).subscribe(res => {
      var getAllChapterResponse = JSON.parse(res._body);
      for(var i=0; i<getAllChapterResponse.responseData.length; i++){
        this.messageList.push(getAllChapterResponse.responseData[i].id +"  "+ getAllChapterResponse.responseData[i].title);
      }
      this.isLoading=false;                   
    });
  }
  //deleteGenres api
  deletegenre(deletegenremodel) {
    this.isLoading=true;
    this._genreService.deletegenre(deletegenremodel).subscribe(res => {
      var deleteGenreResponse = JSON.parse(res._body);
      this.messageList.push(this.prepandText+deleteGenreResponse.responseMessage); 
      this.isLoading=false;                   
    });
  }
  //publishChapter api
  publishchapter(publishmodel) {
    this.isLoading=true;
    this._chapterService.publishchapter(publishmodel).subscribe(res => {
      var publishChapterResponse = JSON.parse(res._body);
      this.messageList.push(this.prepandText+publishChapterResponse.responseMessage); 
      this.isLoading=false;                   
    });
  }

}
