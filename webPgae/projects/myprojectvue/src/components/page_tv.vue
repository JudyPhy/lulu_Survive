<template>
  <div id="page_tv">
    <poster-grid-list :mediaInfoArray=vedioInfoList></poster-grid-list>
  </div>
</template>

<script>
import PosterGridList from './posterGridList'
export default {
  name: 'page_tv',
  components: {
    'poster-grid-list': PosterGridList
  },
  data: function () {
    return {
      vedioInfoList: [{
        name: 'vedio1',
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        img: require('../assets/img/01.jpeg')
      },
      {
        name: 'vedio2vedio2vedio2vedio2vedio2vedio2vedio2',
        url: '',
        img: require('../assets/img/02.jpeg')
      },
      {
        name: 'vedio3',
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        img: require('../assets/img/03.jpg')
      },
      {
        name: 'vedio4',
        url: '',
        img: require('../assets/img/05.jpg')
      },
      {
        name: 'vedio5',
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        img: require('../assets/img/06.jpg')
      },
      {
        name: 'vedio6',
        url: '',
        img: require('../assets/img/06.jpg')
      },
      {
        name: 'vedio7',
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        img: require('../assets/img/01.jpeg')
      },
      {
        name: 'vedio8',
        url: '',
        img: require('../assets/img/03.jpg')
      },
      {
        name: 'vedio9',
        url: '',
        img: require('../assets/img/02.jpeg')
      }]
    }
  }
}
</script>

<style scoped>
#page_tv {
  background-color: red;
  width: 347px;
  height: 480px;
  /*position: absolute;*/
  /*left: 0;*/
  /*top: 0;*/
}
</style>
