<template>
    <div class="btn-group btn-group-sm" role="group">
        <button type="button" class="btn btn-light">&lt;&lt;</button>
        <button type="button" class="btn btn-light" @click="prevPage">
            &lt;
        </button>
        <button
            v-for="(page, index) in pages"
            :key="index"
            type="button"
            class="btn btn-light"
            :class="{ active: currentPage === page }"
            @click="navigateTo(page)"
        >
            {{ page }}
        </button>
        <!--<button type="button" class="btn btn-light">1</button>
        <button type="button" class="btn btn-light active">2</button>
        <button type="button" class="btn btn-light">...</button>
        <button type="button" class="btn btn-light">8</button>
        <button type="button" class="btn btn-light">9</button>-->
        <button type="button" class="btn btn-light" @click="nextPage">
            &gt;
        </button>
        <button type="button" class="btn btn-light">&gt;&gt;</button>
    </div>
</template>

<script>
export default {
    data() {
        return {
            currentPage: 1,
            itemsSkipArray: [],
        };
    },
    props: {
        itemsCount: Number,
        pageSize: Number,
    },
    emits: ["navigate"],
    computed: {
        pages() {
            return Math.ceil(this.itemsCount / this.pageSize);
        },
    },
    methods: {
        navigateTo(page) {
            this.currentPage = page;
            var skipValue = this.itemsSkipArray.find((x) => x.key === page);

            this.$emit("navigate", { skip: skipValue.value.skip });
        },
        prevPage() {
            let targetPage = this.currentPage - 1;

            this.navigateTo(targetPage <= 0 ? 1 : targetPage);
        },
        nextPage() {
            let targetPage = this.currentPage + 1;

            this.navigateTo(targetPage >= this.pages ? this.pages : targetPage);
        },
        rewind() {
            this.currentPage = 1;
            this.initialize();
        },
        initialize() {
            this.itemsSkipArray = [];
            for (let i = 0; i < this.pages; i++) {
                let skip = i * this.pageSize;
                this.itemsSkipArray.push({
                    key: i + 1,
                    value: { skip },
                });
            }
            this.navigateTo(this.currentPage);
        },
    },
    watch: {
        pageSize() {
            this.rewind();
        },
    },
    created() {
        this.initialize();
    },
};
</script>
