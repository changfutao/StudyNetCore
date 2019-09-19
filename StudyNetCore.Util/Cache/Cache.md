# MemoryCache

IMemoryCache提供了三个方法并实现了IDisposable接口
1.CreateEntry 添加一个缓存
2.Remove 删除一个缓存
3.TryGetValue 获取一个缓存(并可得到具体得缓存是否存在)

### ICacheEntry成员
+ Key 缓存key

+ Value 缓存值

+ AbsoluteExpiration 绝对过期时间，为null则条件无效

+ AbsoluteExpirationRelativeToNow 相对当前时间的绝对过期时间（使用TimeSpan），为null条件无效

+ SlidingExpiration 滑动过期时间

+ ExpirationTokens 提供用来自定义缓存过期

+ PostEvictionCallbacks 缓存失效回调

+ Priority 缓存项优先级（在缓存满载的时候绝对清除的顺序）

+ Size 代表缓存数据的大小，在内存缓存中一般为null
