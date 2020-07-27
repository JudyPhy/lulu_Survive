<template>
    <div class="item_nav" :style="{'background-color':this.bg_color}">
      <p>{{this.title}}</p>
    </div>
</template>

<script>
export default {
  name: 'item_nav',
  props: {
    name: String,
    bgColor: String
  },
  data: function () {
    return {
      title: this.name,
      bg_color: this.bgColor
    }
  }
}
</script>

<style scoped>
.item_nav {
  width: 100%;
  height: 96px;
}
.item_nav p {
  font-size: 1rem;
  color: white;
  padding-top: 5px;
}
</style>
