import { Injectable, OnInit } from '@angular/core';
import { Topic } from '../models/topic';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class TopicsProviderService {
    constructor(private httpClient: HttpClient) { }

    get Topics(): Promise<Topic[]> {
        let topicsUrl = `${environment.config.apiDomain}${environment.config.topicsPath}`;
        let promise = new Promise<Topic[]>((resolve, reject) => {
            this.httpClient
                .get(topicsUrl)
                .subscribe((response: Topic[]) => {
                    resolve(response);
                }, err => reject(err));
        });

        return promise;
    }

} 