<template>
  <div>
    <poster-grid-list :vedioInfoArray="this.vedioInfoList"></poster-grid-list>
  </div>
</template>

<script>
import PosterGridList from './posterGridList'
export default {
  name: 'page_recommend',
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
        img: require('../assets/img/03.jpg')
      },
      {
        name: 'vedio3',
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        img: require('../assets/img/05.jpg')
      },
      {
        name: 'vedio4',
        url: '',
        img: require('../assets/img/06.jpg')
      }]
    }
  }
}
</script>

<style scoped>

</style>
