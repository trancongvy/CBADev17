declare @tungay datetime
declare @denngay datetime
declare @MaVT varchar(16)
declare @MaKHO varchar(16)
set @tungay='01/01/2014'
set @denngay='03/31/2014'
set @makho='1561'
set @mavt='PEPSI'
--xóa những mapid đã tính trong kỳ
delete blvtmapid where ngayctX>=@tungay 
--Lấy số dư đầu kỳ
select identity(int,1,1) as stt,*, soluong_N-soluong_X as Conlai into #n from (
	select x.*,soluong_X= case when y.soluong_X is null then 0.0 else y.soluong_X end
	from 
	(
		select obntxtid as MTID_N, OBNTXTid as DTID_N, SOCT as NhomDK_N, soluong as soluong_N,ngayct,soct
			from obntxt where mavt=@mavt and makho=@makho
		union all
		select mtid , mtiddt, nhomdk, soluong, ngayct,soct
		from blvt where mavt=@mavt and makho=@makho and (soluong>0 or psno>0)
	) x left join 
	(select MTID_N, DTID_N, NHOMDK_N, sum(soluong_X) as soluong_X from blvtmapid group by MTID_N, DTID_N, NHOMdk_N
	)y on x.MTID_N=y.MTID_N and x.dtid_n=y.dtid_n and x.nhomdk_N=y.nhomdk_N
)z where soluong_N>soluong_X order by ngayct, soct
--
select * from #n
--Lấy phần xuất trong kỳ
select identity(int,1,1) as stt,mtid , mtiddt as dtid, nhomdk,soluong_x, ngayct,soct
		into #x from blvt where mavt=@mavt and makho=@makho and (soluong_x>0 or (soluong_x=0 and psco>0))
		order by ngayct, soct
select * from #x
declare @RowX int 
 select @RowX= Max(stt) from #x
 declare @RowN int 
 select @RowN= Max(stt) from #N
 print @rowx
declare @sttN int 
set @sttN=1
declare @sttX int 
set @sttX=1
	
while(@sttX<=@RowX)
begin
	declare @MTID uniqueidentifier
	declare @DTID uniqueidentifier
	declare @NhomDK_X nvarchar(16)
	declare @soluong_X float
	declare @ngayctX datetime
	select @mtid=mtid, @dtid =dtid, @nhomdk_X=nhomdk,@soluong_X=soluong_x, @ngayctX=ngayct from #x where stt=@sttx
	declare @conlai float
	set @conlai = @Soluong_X

	while (@sttN<=@RowN and @conlai>0)
	begin
		declare @MTID_N uniqueidentifier
		declare @DTID_N uniqueidentifier
		declare @NhomDK_N nvarchar(16)
		declare @soluong_N float
			select @mtid_N=mtid_N, @dtid_N =dtid_N, @nhomdk_N=nhomdk_N,@soluong_N=Conlai from #N where stt=@sttN
		
		if(@conlai<@soluong_N)
		begin			
			
			insert into blvtmapID (mtid_N, DTID_N, Nhomdk_N,mtid_X, DTID_X, Nhomdk_X,soluong_X, ngayctx)
				values (@mtid_N, @DTID_N, @Nhomdk_N,@mtid, @DTID, @Nhomdk_X,@Conlai, @ngayctx)
				update #N set Conlai=@soluong_n-@Conlai where stt=@sttN
			set @conlai=0

		end
		else
		begin
			update #n set conlai=0  where stt=@sttN
			insert into blvtmapID (mtid_N, DTID_N, Nhomdk_N,mtid_X, DTID_X, Nhomdk_X,soluong_X, ngayctx)
				values (@mtid_N, @DTID_N, @Nhomdk_N,@mtid, @DTID, @Nhomdk_X,@Soluong_N, @ngayctx)
			set @conlai=@conlai-@soluong_n  
			set @sttN=@sttN+1

		end
	end
	set @sttx =@sttx+1
end
select * from blvtmapid order by stt

drop table #n
drop table #x