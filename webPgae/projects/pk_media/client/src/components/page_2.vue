<template>
    <div>
      <d-player ref="player" @play="play" :video="video" :contextmenu="contextmenu"></d-player>
    </div>
</template>

<script>
import VueDPlayer from './VueDPlayerHls'
export default {
  name: 'page_2',
  data () {
    return {
      msg: 'Welcome to Your Vue.js App',
      video: {
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        pic: '',
        type: 'auto'
      },
      autoplay: false,
      player: null,
      contextmenu: [
        {
          text: 'Play',
          link: 'http://www.hellojava.com/a/85349.html'
        }
      ]
    }
  },
  components: {
    'd-player': VueDPlayer
  },
  methods: {
    play () {
      console.log('play callback')
    }
  },
  mounted () {
    // this.player = this.$refs.player.dp
    // // console.log(this.player);
    // var hls = new Hls()
    // hls.loadSource('https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8')
    // hls.attachMedia(this.player)
  }
}
</script>

<style scoped>

</style>
