<template>
  <div>
    <d-player ref="player" @play="play" :video="video" :contextmenu="contextmenu"></d-player>
  </div>
</template>

<script>
import VueDPlayer from './VueDPlayerHls'
export default {
  name: 'singleVedio',
  components: {
    'd-player': VueDPlayer
  },
  props: {
    vedioUrl: {
      type: String,
      default: ''
    }
  },
  data: function () {
    return {
      video: {
        url: this.vedioUrl,
        pic: '',
        type: 'auto'
      },
      autoplay: false,
      player: null,
      contextmenu: []
    }
  },
  methods: {
    play: function () {
      console.log('play')
    }
  }
}
</script>

<style scoped>

</style>
