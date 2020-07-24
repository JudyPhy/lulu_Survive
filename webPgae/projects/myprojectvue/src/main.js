// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'

// import Video from 'video.js'
import VideoPlayer from 'vue-video-player'
import 'video.js/dist/video-js.css' // videoJs的样式
import 'vue-video-player/src/custom-theme.css' // vue-video-player的样式
import hls from 'videojs-contrib-hls'
import VueResource from 'vue-resource'

Vue.config.productionTip = false

Vue.use(VideoPlayer)
Vue.use(hls)
Vue.use(VueResource)

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
