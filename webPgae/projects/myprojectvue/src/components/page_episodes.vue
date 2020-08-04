<template>
  <div id="page_episodes">
    <scroll_container :mediaDataArray=vedioInfoList></scroll_container>
  </div>
</template>

<script>
import ScrollContainer from './container_scroll'
export default {
  name: 'page_episodes',
  components: {
    'scroll_container': ScrollContainer
  },
  data: function () {
    return {
      vedioInfoList: [{
        name: '一一一一',
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        img: require('../assets/img/01.jpeg'),
        dataId: ''
      },
      {
        name: '二二二二',
        url: '',
        img: require('../assets/img/02.jpeg'),
        dataId: ''
      },
      {
        name: '三三三三',
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        img: require('../assets/img/03.jpg'),
        dataId: ''
      },
      {
        name: '四四四四',
        url: '',
        img: require('../assets/img/05.jpg'),
        dataId: ''
      },
      {
        name: '五五五五',
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        img: require('../assets/img/06.jpg'),
        dataId: ''
      },
      {
        name: '六六六六六六六',
        url: '',
        img: require('../assets/img/06.jpg'),
        dataId: ''
      },
      {
        name: '七七七七',
        url: 'https://d.mahua-kb.com/20200708/DpGB4wBv/index.m3u8',
        img: require('../assets/img/01.jpeg'),
        dataId: ''
      },
      {
        name: '八八八八',
        url: '',
        img: require('../assets/img/03.jpg'),
        dataId: ''
      },
      {
        name: '九九九九',
        url: '',
        img: require('../assets/img/02.jpeg'),
        dataId: ''
      }]
    }
  }
}
</script>

<style scoped>
#page_episodes {
  width: 347px;
  height: 480px;
  overflow: hidden;
  position: relative;
}
</style>
