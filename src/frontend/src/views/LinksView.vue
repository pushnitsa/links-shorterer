<template>
    <div class="d-flex pt-3 pb-2 mb-3 border-bottom">
        <h1>Exist links</h1>
    </div>
    <ShortLinkList />
</template>

<script>
import ShortLinkList from "@/components/ShortLinkList.vue";

export default {
    components: { ShortLinkList },
};
</script>
