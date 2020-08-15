import { apiBase } from "../variables";

export class MotionPictureApi {

    constructor() {

    }

    getMotionPictures() {
        const request = new XMLHttpRequest();
        request.open(HttpMethod.GET, apiBase + 'MotionPictures');
        request.send();
        return request.response;
    }
}

export class HttpMethod {
    static GET = 'GET';
    static POST = 'POST';
    static PUT = 'PUT';
    static DELETE = 'DELETE'; 
}