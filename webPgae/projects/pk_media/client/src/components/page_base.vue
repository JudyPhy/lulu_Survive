<template>
  <div class="page_base">
    <ContentContainer></ContentContainer>
  </div>
</template>

<script>
import ContentContainer from './container_dropdown'
export default {
  name: 'page_base',
  components: {ContentContainer},
  data: function () {
    return {
    }
  }
}
</script>

<style scoped>

</style>
