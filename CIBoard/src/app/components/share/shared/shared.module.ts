import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {  RouterModule } from '@angular/router';
import {ApploaderComponent} from './../../share/apploader/apploader.component';
import {AppheaderComponent} from './../../../components/share/appheader/appheader.component';
import {AppfooterComponent} from './../../../components/share/appfooter/appfooter.component';
 
@NgModule({
    declarations: [AppheaderComponent,AppfooterComponent,ApploaderComponent],
    exports:[AppheaderComponent,AppfooterComponent,ApploaderComponent],
    imports: [        
        RouterModule,
        CommonModule      
    ]
  })
  export class SharedModule { }