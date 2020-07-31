<template>
  <div id="page_tv">
    <grid_33 :dataArray=vedioInfoList></grid_33>
  </div>
</template>

<script>
import Grid33 from './grid_33'
export default {
  name: 'page_tv',
  components: {
    'grid_33': Grid33
  },
  data: function () {
    return {
      vedioInfoList: [{
        name: '一一一一',
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        img: require('../assets/img/01.jpeg')
      },
      {
        name: '二二二二',
        url: '',
        img: require('../assets/img/02.jpeg')
      },
      {
        name: '三三三三',
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        img: require('../assets/img/03.jpg')
      },
      {
        name: '四四四四',
        url: '',
        img: require('../assets/img/05.jpg')
      },
      {
        name: '五五五五',
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        img: require('../assets/img/06.jpg')
      },
      {
        name: '六六六六六六六',
        url: '',
        img: require('../assets/img/06.jpg')
      },
      {
        name: '七七七七',
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        img: require('../assets/img/01.jpeg')
      },
      {
        name: '八八八八',
        url: '',
        img: require('../assets/img/03.jpg')
      },
      {
        name: '九九九九',
        url: '',
        img: require('../assets/img/02.jpeg')
      }]
    }
  }
}
</script>

<style scoped>
#page_tv {
  width: 347px;
  height: 480px;
  overflow: hidden;
}
</style>
