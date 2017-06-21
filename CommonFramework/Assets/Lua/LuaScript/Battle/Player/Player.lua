---
--- Created by luzhuqiu.
--- DateTime: 2017/6/19 下午2:46
---

require 'Battle/Player/Logic/PlayerLogicHash'
require 'Battle/Player/Logic/PlayerLogicHashItem'
require 'Battle/Player/Render/PlayerRenderHash'
require 'Battle/Player/Render/PlayerRenderHashItem'
require 'Battle/Player/PositionArray'

Player = class(BattleLogicObject)

function Player:Init(id,long,startPos)
    self.id = id
    self.logicHash = PlayerLogicHash.new()
    self.renderHash = PlayerRenderHash.new()

    ---每一节的间隔距离
    self.partSpace = 0.3
    ---行进速度
    self.runSpeed = 0.3
    ---旋转速度
    self.rotateSpeed = 0.1
    ---多长时间改变一次位置
    self.refreshPosPerTime = 0.5
    ---刷新位置时间缓存
    self.refreshPosTimeCache = 0

    self.rotation = nil

    if startPos.x >= 0 then
        self.rotation = Quaternion.Euler(0,0,90)
    else
        self.rotation = Quaternion.Euler(0,0,-90)
    end

    self.posX = startPos.x
    self.posY = startPos.y

    for i = 1,long,1 do

        local logicItem = PlayerLogicHashItem.new()
        local renderItem = PlayerRenderHashItem.new()

        self.logicHash:Add(i,logicItem)
        self.renderHash:Add(i,renderItem)

        renderItem.totalFrame = self.refreshPosPerTime

    end

    self.positionLogicArray = PositionArray.new()
    self.positionLogicArray:SetSize(long)

    for i = 1,long,1 do
        self.positionLogicArray:Push(self.posX,self.posY,self.rotation)
    end

    self.positionRenderArray = PositionArray.new()
    self.positionRenderArray:SetSize(long)

    for i = 1,long,1 do
        self.positionRenderArray:Push(self.posX,self.posY,self.rotation)
    end
end

function Player:SetSize(size)
    self.positionLogicArray:SetSize(size)
    self.positionRenderArray:SetSize(size)
end

function Player:Move(time)

    self.posX = self.posX + time * self.runSpeed
    self.refreshPosTimeCache = self.refreshPosTimeCache + time
    if self.refreshPosTimeCache >= self.refreshPosPerTime then
        self.refreshPosTimeCache = 0
        self.positionLogicArray:Push(self.posX,self.posY,self.rotation)
        self.positionRenderArray:Push(self.posX,self.posY,self.rotation)

        BattleRenderManager:GetInstance():RefreshTarget(self.id)
    end

end