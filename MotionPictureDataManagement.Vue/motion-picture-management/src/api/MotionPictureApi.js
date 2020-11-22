import axios from 'axios';
import router from '../router';
import { apiBase } from '../../variables';

export class MotionPictureApi {
    constructor() {

    }

    static getMotionPictures() {
        return axios
            .get(apiBase + "MotionPictures");
    }
    static getMotionPictureById(id) {
        return axios
            .get(`${apiBase}MotionPicture/${id}`)
    }

    static postMotionPicture(motionPicturePost) {
        return axios
            .post(`${apiBase}MotionPicture`, motionPicturePost)
            .then(() => router.push("/"));
    }

    static putMotionPicture(id, motionPicturePut) {
        return axios
            .put(`${apiBase}MotionPicture/${id}`, motionPicturePut)
            .then(() => router.push("/"));
    }

    static deleteMotionPicture(id) {
        return axios
            .delete(`${apiBase}MotionPicture/${id}`);
    }
}
