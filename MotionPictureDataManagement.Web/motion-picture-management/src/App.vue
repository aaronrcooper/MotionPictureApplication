<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png">
    <MotionPictureTable />
  </div>
</template>

<script>
import MotionPictureTable from './components/MotionPictureTable.vue';

export default {
  name: 'App',
  components: {
    MotionPictureTable
  },
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
