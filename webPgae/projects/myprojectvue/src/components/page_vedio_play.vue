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
  methods: {
    backToBeforePage: function () {
      this.$router.go(-1)
    }
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
