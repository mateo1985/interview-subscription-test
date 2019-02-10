import { ToastrService } from 'ngx-toastr';
import { Injectable, ViewContainerRef } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class NotificationService {
    constructor(public toastr: ToastrService) {
        
     }
       
     showSuccess(message: string, title: string) {
       this.toastr.success(message, title);
     }
   
     showError(message: string, title: string) {
       this.toastr.error(message, title);
     }
   
     showWarning(message: string, title: string) {
       this.toastr.warning(message, title);
     }
   
     showInfo(message: string, title: string) {
       this.toastr.info(message, title);
     }
}