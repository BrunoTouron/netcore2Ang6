import { Injectable } from '@angular/core';
import { constructor } from 'events';

@Injectable({
    providedIn: 'root'
})

export class ConfigurationParameters {

    constructor() {

    }

    getApiUrl() {
        return 'https://localhost:44323/api/';
    }
}
