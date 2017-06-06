---
--- Created by luzhuqiu.
--- DateTime: 2017/6/5 下午2:22
---
---
---协议号
----0,99 tcp系统相关
----100,199 udp系统相关
----    100 udp请求重发
----    101 udp心跳
----200,1000 tcp功能相关
----1000,~ udp功能相关
----    1000 udp测试 UdpPackage


require 'SystemModule/Network/UDPDataPacket'
require 'SystemModule/Network/LinkUDPPackets'
require 'Proto/UdpLostPackage_pb'

UdpNetwork = class()

local timeSendAgainDelta = 0.1
local timeHeartbeatDelta = 5

function UdpNetwork:GetInstance()
    if self.m_instance == nil then
        self.m_instance = UdpNetwork.new()
    end
    return self.m_instance
end

local function SendRequestSendAgainUpdate(self)
    if Time.time - self.timeCache > timeSendAgainDelta then
        self:SendRequestSendAgain()
        self.timeCache = Time.time
    end
end

function UdpNetwork:Init(sendSucess,receiveCallback)

    self.timeCache = 0
    self.sendSeq = 0
    self.sendLink = LinkUDPPackets.new()
    self.receiveLink = LinkUDPPackets.new()

    UDPServer.Instance:InitUDPServer('127.0.0.1',1111,'127.0.0.1',1110)
    local funcSendSucess = function()
        sendSucess()
    end
    local funcReceiveCallback = function(data)
        local pack = UDPDataPacket.new()
        pack:UnPack(data)

        if pack.id == 100 then
            local lostPackage = UdpLostPackage_pb.UdpLostPackage()
            lostPackage:ParseFromString(pack.data)
            for i=1,#lostPackage.listSeqID do
                Debugger.LogError('收到重发包请求 seqid : ' .. lostPackage.listSeqID[i])
                self:ReSend(lostPackage.listSeqID[i])
            end
        elseif pack.id > 200 then
            self.receiveLink:Insert(pack)
            self.receiveLink:CheckLost(pack)
            --for k,v in pairs(self.receiveLink.lostSeq) do
            --    Debugger.LogError('self.receiveLink.lostSeq  ' .. k)
            --end
            if receiveCallback == nil then
                Debugger.LogError('receiveCallback == nil')
            else
                receiveCallback(pack)
            end
        end

    end
    UDPServer.Instance:SetSendSucessCallback(funcSendSucess)
    UDPServer.Instance:SetReceive(funcReceiveCallback)

    FixedUpdateBeat:Add(SendRequestSendAgainUpdate, self)
end

function UdpNetwork:Stop()
    FixedUpdateBeat:Remove(SendRequestSendAgainUpdate,self)
end

--心跳
function UdpNetwork:SendHeartbeat()

end

--请求服务端重发包
function UdpNetwork:SendRequestSendAgain()

    if next(self.receiveLink.lostSeq) ~= nil then

        local packet = UDPDataPacket.new()
        local lostPackage = UdpLostPackage_pb.UdpLostPackage()
        for k,v in pairs(self.receiveLink.lostSeq) do
            table.insert(lostPackage.listSeqID,k)
            Debugger.LogError('--请求服务端重发包 ' .. k)
        end

        local data = lostPackage:SerializeToString()
        packet:Pack(100,0,data)
        UDPServer.Instance:SendUDPMsg(packet.data)
    end

end

--重发队列里面的包
function UdpNetwork:ReSend(seq)
    local packet = self.sendLink:GetPacketBySeq(seq,true)
    if packet ~= nil then
        UDPServer.Instance:SendUDPMsg(packet.data)
    end
end

--发送一个新包
function UdpNetwork:Send(id,data)
    local packet = UDPDataPacket.new()

    packet:Pack(id,self.sendSeq,data)
    self.sendLink:Insert(packet)

    self.sendSeq = self.sendSeq + 1

    UDPServer.Instance:SendUDPMsg(packet.data)
end