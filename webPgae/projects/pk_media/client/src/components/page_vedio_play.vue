<template>
  <div id="page_vedio_play">
    <div class="player">
      <p class="btnBack" @click="backToBeforePage">返回</p>
      <div></div>
      <div>
        <h2>Title</h2>
        <p>detail info</p>
      </div>
    </div>
    <div class="recommend">
      推荐
    </div>
  </div>
</template>

<script>
export default {
  name: 'page_vedio_play',
  data: function () {
    return {
      curMediaUrl: ''
    }
  },
  methods: {
    getMediaPlayUrl: function () {
      let dataId = this.$route.query.dataId
      this.$http.post('/api/posterInfo/search').then(response => {
        console.log(response.data)
        this.vedioInfoList = []
        for (var i = 0; i < 9; i++) {
          this.vedioInfoList.push(response.data[i + 20])
        }
      }, response => {
        console.log('error')
      })
    },
    backToBeforePage: function () {
      this.$router.go(-1)
    }
  },
  mounted () {
    this.curMediaUrl = this.getMediaPlayUrl()
  }
}
</script>

<style scoped>
.player {
  background-color: darkblue;
  width: 347px;
  height: 200px;
  position: relative;
}
.btnBack {
  position: absolute;
  top: 0;
  left: 0;
}
</style>
