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
  created () {
    console.log('1111111111')
    this.getParams()
  },
  methods: {
    play: function () {
      console.log('play')
    },
    getParams () {
      const routerParams = this.$route.query.id
      console.log('接受的pdf的值：', routerParams)
    }
  }
}
</script>

<style scoped>

</style>
