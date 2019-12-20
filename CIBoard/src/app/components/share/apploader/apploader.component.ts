import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-apploader',
  templateUrl: './apploader.component.html',
  styleUrls: ['./apploader.component.css']
})
export class ApploaderComponent implements OnInit {
  @Input() message:String;
  @Input() bgnumber:String;
  @Input() textcolor:String;
  constructor() { 
    if(!this.bgnumber){
    this.bgnumber="0";
    }

    if(!this.textcolor){
      this.textcolor='#FFFFFF';
    }
  }
  ngOnInit() {
  }
}