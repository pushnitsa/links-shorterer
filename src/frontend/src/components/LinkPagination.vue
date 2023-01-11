<template>
    <div class="btn-group btn-group-sm" role="group">
        <button type="button" class="btn btn-light" @click="toFirstPage()">
            &lt;&lt;
        </button>
        <button type="button" class="btn btn-light" @click="prevPage()">
            &lt;
        </button>
        <template v-for="(page, index) in pages" :key="index">
            <button v-if="page.breakView" type="button" class="btn btn-light">
                {{ page.value }}
            </button>
            <button
                v-else
                type="button"
                class="btn btn-light"
                :class="{ active: currentPage === page.value }"
                @click="navigateTo(page.value)"
            >
                {{ page.value }}
            </button>
        </template>
        <button type="button" class="btn btn-light" @click="nextPage()">
            &gt;
        </button>
        <button type="button" class="btn btn-light" @click="toLastPage()">
            &gt;&gt;
        </button>
    </div>
</template>

<script>
export default {
    data() {
        return {
            currentPage: 1,
        };
    },
    props: {
        itemsCount: Number,
        pageSize: Number,
    },
    emits: ["navigate"],
    computed: {
        pages() {
            let items = [];

            /**
             * [1,2,...,9,10]           cur: 1, amount: 5
             * [1,2,3,...,9,10]         cur: 2, amount: 6
             * [1,2,3,4,5,...,9,10]     cur: 4, amount: 8
             * [1,2,3,4,5,6,7,...,9,10] cur: 6, amount: 10
             * [1,2,3,4,5,6,7,8,9,10]   cur: 7, amount: 10
             */
            if (this.pageCount > 5) {
                const pagesToExclude = 3;
                const minimumPagesInTheEndToIncludeBreakView = 4;

                for (
                    let i = 0;
                    // Exclude last three pages, we'll handle its manually
                    i <= this.currentPage &&
                    i < this.pageCount - pagesToExclude;
                    i++
                ) {
                    items.push({ value: i + 1 });
                }

                // Has to be at least 4 more pages to include break view
                const includeBreakView =
                    this.currentPage + minimumPagesInTheEndToIncludeBreakView <=
                    this.pageCount;

                if (includeBreakView) {
                    items.push({ breakView: true, value: "..." });
                }

                let includingEndPages = this.pageCount - 1;
                if (!includeBreakView) {
                    --includingEndPages;
                }

                for (let i = includingEndPages; i <= this.pageCount; i++) {
                    items.push({ value: i });
                }
            } else {
                for (let i = 1; i <= this.pageCount; i++) {
                    items.push({ value: i });
                }
            }

            return items;
        },
        pageCount() {
            return Math.ceil(this.itemsCount / this.pageSize);
        },
    },
    methods: {
        navigateTo(page) {
            this.currentPage = page;

            this.$emit("navigate", { skip: (page - 1) * this.pageSize });
        },
        toFirstPage() {
            this.navigateTo(1);
        },
        prevPage() {
            let targetPage = this.currentPage - 1;

            this.navigateTo(targetPage <= 0 ? 1 : targetPage);
        },
        nextPage() {
            let targetPage = this.currentPage + 1;

            this.navigateTo(
                targetPage >= this.pageCount ? this.pageCount : targetPage
            );
        },
        toLastPage() {
            this.navigateTo(this.pageCount);
        },
        rewind() {
            this.currentPage = 1;
            this.initialize();
        },
        initialize() {
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
