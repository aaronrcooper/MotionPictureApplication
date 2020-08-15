<template>
  <tr>
    <th scope="row">{{motionPicture.id}}</th>
    <td>{{motionPicture.name}}</td>
    <td>{{motionPicture.description}}</td>
    <td>{{motionPicture.releaseYear}}</td>
    <td class="actions">
      <i class="fas fa-edit"></i>
      <i class="far fa-copy"></i>
      <i class="fas fa-trash-alt" v-on:click="deleteMotionPicture(motionPicture.id)"></i>
    </td>
  </tr>
</template>

<script>

import {apiBase } from '../../variables';
import axios from 'axios';

export default {
  name: "MotionPictureTableRow",
  props: {
    motionPicture: Object
  },
  methods: {
    deleteMotionPicture: function (id) {
      axios
        .delete(`${apiBase}MotionPicture/${id}`)
        .then(() => {
          // Emit to parent that we removed this item
          this.$emit('removed', id);
        })
        .catch();
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}

.actions i {
  margin: 0px 4px;
}

.actions i:hover {
  cursor: pointer;
}
</style>
