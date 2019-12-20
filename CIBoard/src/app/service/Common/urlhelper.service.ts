import { environment } from '../../../environments/environment';
export class UrlHelperService {
    static baseUrl=environment.base_url; 
    
    static login='login';
    static createnovel='novel/createnovel';
    static getavailablenovels='novel/getavailablenovels';
    static creategenre='genre/creategenre';
    static deletegenre='genre/deletegenre';
    static getallgenres='genre/getallgenres';
    static getgenressearchedbyuser='genre/getgenressearchedbyuser';
    static createchapter='chapter/createchapter';
    static getallchapters='chapter/getallchapters';
    static publishchapter='chapter/publishchapter';
    static alluser='superadminusermanagement/getalluserslist';
    static roleManagment = 'superadminusermanagement/getuserrolelist';
    static activeDectiveUser = 'superadminusermanagement/verifystatuschangebyuserid';
    static adduser = 'superadminusermanagement/adduserbyadminoropereationteam';
    static deleteUser = 'superadminusermanagement/deleteuser';
    static getcustomerdetail = 'superadminusermanagement/getcustomerdetail';
   
}