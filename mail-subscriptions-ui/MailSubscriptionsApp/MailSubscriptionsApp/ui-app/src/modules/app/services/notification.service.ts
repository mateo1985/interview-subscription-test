import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class NotificationService {
    constructor(public toastr: ToastrService) { }
       
    showSuccess(title: string, message: string) {
      this.toastr.success(message, title);
    }
  
    showError(title: string, message: string) {
      this.toastr.error(message, title);
    }
  
    showWarning(title: string, message: string) {
      this.toastr.warning(message, title);
    }
  
    showInfo(title: string, message: string) {
      this.toastr.info(message, title);
    }
}