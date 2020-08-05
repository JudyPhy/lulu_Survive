<template>
  <div id="page_tv">
    <scroll_container :mediaDataArray=vedioInfoList></scroll_container>
  </div>
</template>

<script>
import ScrollContainer from './container_scroll'
export default {
  name: 'page_tv',
  components: {
    'scroll_container': ScrollContainer
  },
  data: function () {
    return {
      vedioInfoList: []
    }
  },
  methods: {
    getPosterInfo: function () {
      this.$http.post('/api/posterInfo/search').then(response => {
        console.log(response.data)
        this.vedioInfoList = []
        for (var i = 0; i < 9; i++) {
          this.vedioInfoList.push(response.data[i + 20])
        }
      }, response => {
        console.log('error')
      })
    }
  },
  created () {
    this.getPosterInfo()
  }
}
</script>

<style scoped>
#page_tv {
  width: 347px;
  height: 480px;
  overflow: hidden;
  position: relative;
}
</style>
