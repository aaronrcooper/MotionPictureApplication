import { apiBase } from 'variables';

Vue.component('table', {
    props: ['motionPictures'],
    template: './table.html'
})

var table = new Vue({
    el: 'table',
    data () {
        return {
            motionPictures: null
        };
    },
    mounted () {
        axios
            .get(apiBase + 'MotionPictures')
            .then(response => (this.data.motionPictures = response));
    }
});

