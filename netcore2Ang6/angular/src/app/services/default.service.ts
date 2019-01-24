import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DefaultModel } from '../models/default';
import { ConfigurationParameters } from '../singleton/configuration.singleton';

@Injectable({
    providedIn: 'root'
})
export class DefaultService {
    routeUrl = '';

    constructor(protected configuration: ConfigurationParameters,
        protected apiConnection: HttpClient) {
        console.log('default create');
    }

    public setRoute(route: string) {
        this.routeUrl = this.configuration.getApiUrl() + route;
    }

    public getFilter(parameters: string): Observable<DefaultModel[]> {
        return this.apiConnection.get<DefaultModel[]>(this.routeUrl + '/' + parameters);
    }

    public async getExist(parametersObject: {}): Promise<boolean> {
        let parameters = '';
        let exist = false;

        for (const key in parametersObject) {
            if (parametersObject.hasOwnProperty(key)) {
                if (parameters !== '') {
                    parameters = parameters + '/';
                }

                let value = parametersObject[key];

                if (value === '') {
                    value = 'null';
                }

                parameters = parameters + value;
            }
        }

        await this.apiConnection.get<boolean>(this.routeUrl + '/exists/' + parameters).toPromise().then(
            (valuesAPI) => {
                exist = valuesAPI;
            });

        return exist;
    }

    public get(): Observable<DefaultModel[]> {
        return this.apiConnection.get<DefaultModel[]>(this.routeUrl);
    }

    public getByRoute(route: string): Observable<DefaultModel[]> {
        return this.apiConnection.get<DefaultModel[]>(this.configuration.getApiUrl() + route);
    }

    public insert(model: DefaultModel): Observable<Object> {
        return this.apiConnection.post(this.routeUrl, model);
    }

    public delete(id: number): Observable<Object> {
        return this.apiConnection.delete(this.routeUrl + '/' + id);
    }

    public getById(id: number): Observable<DefaultModel> {
        return this.apiConnection.get<DefaultModel>(this.routeUrl + '/' + id);
    }

    public getByIdRoute(route: string, id: number): Observable<DefaultModel> {
        return this.apiConnection.get<DefaultModel>(this.configuration.getApiUrl() + route + '/' + id);
    }

    public update(model: DefaultModel): Observable<Object> {
        return this.apiConnection.put(this.routeUrl + '/' + model.id, model);
    }

    public save(model: DefaultModel): Observable<Object> {
        if (model.id === 0) {
            return this.insert(model);
        } else {
            return this.update(model);
        }
    }
}
