import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class SubscriptionsService {
    constructor(private httpClient: HttpClient) { }

    subscribe(topics: number[], userEmail: string): Observable<any> {
        console.log('subscriptions:', topics, userEmail);
        return this.httpClient.post(`${environment.config.apiDomain}${environment.config.subscriptionsPath}`, {
            UserId: userEmail,
            Topics: topics
        })
    }
} 